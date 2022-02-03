#region Access
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using XavHelpTo;
using XavHelpTo.Set;
using XavHelpTo.Change;
using StorageData;
using UnityEngine.Networking;

#endregion
#region ### DataPass
/// <summary>
/// Encargado de ser la conexión de los datos guardados con las escenas
/// Este podrá cargar el ultimo archivo o guardar un archivo con sus datos
/// <para>Dependencias: <seealso cref="Environment.Data"/>, <seealso cref="SavedData"/>, <seealso cref="DataStorage"/>, <seealso cref="LocalData"/>, <seealso cref="OnlineData"/></para>
/// </summary>
public class DataSystem : MonoBehaviour
{
    #region ####### VARIABLES
    private static DataSystem _;

    private const string savedPath = "saved.txt";
    [SerializeField] private SavedData savedData = new SavedData();
    [Space(50)]
    [SerializeField] private LocalData localData = new LocalData();
    private static Action<SavedData> OnSyncSaved;
    #endregion
    #region ###### EVENTS
    private void Awake()
    {
        this.Singleton(ref _,true);
        _._SaveLoadFile(!File.Exists(Path));
    }
    #endregion
    #region ####### METHODS
    /// <returns>The path of the saved data</returns>
    internal static string Path => Application.persistentDataPath + savedPath;
    /// <summary>
    /// Save or loads the files
    /// </summary>
    private void _SaveLoadFile(in bool wantSave = false)
    {
        BinaryFormatter _formatter = new BinaryFormatter();
        FileStream _stream = new FileStream(Path, wantSave ? FileMode.Create : FileMode.Open);
        DataStorage _dataStorage;

        //Dependiendo de si va a cargar o guardar hará algo o no
        if (wantSave)
        {
            _dataStorage = new DataStorage(GetSaved);
            _formatter.Serialize(_stream, _dataStorage);
            _stream.Close();
            OnSyncSaved?.Invoke(GetSaved);
        }
        else
        {
            _dataStorage = _formatter.Deserialize(_stream) as DataStorage;
            _stream.Close();
            SetSaved(_dataStorage.savedData);

            // refresh editor view
            #if UNITY_EDITOR
                UnityEditor.AssetDatabase.Refresh();
            #endif
        }
    }

    /// <summary>
    ///  Update the Data of <see cref="DataPass"/> in <seealso cref="SavedData"/> with <paramref name="newSavedData"/>
    /// </summary>
    public static void SetSaved(SavedData newSavedData) => _.savedData = newSavedData;
    /// <returns>The Loaded data in <see cref="DataPass"/></returns>
    public static SavedData GetSaved => _.savedData;
    /// <summary>
    /// Save the data
    /// </summary>
    public static void Save() => _._SaveLoadFile(true);
    /// <summary>
    /// Load the data
    /// </summary>
    public static void Load() => _._SaveLoadFile(false);
    /// <summary>
    /// Delete the file
    /// </summary>
    public static void Delete() => File.Delete(Path);

    /// <summary>
    /// Debug Pointer to save the <see cref="DataStorage"/> file
    /// </summary>
    [ContextMenu("Guardar los datos")] public void _Save() => Save();
    /// <summary>
    /// Debug Pointer to load the saved values
    /// </summary>
    [ContextMenu("Cargar los datos")] public void _Load() => Load();
    /// <summary>
    /// Debug Pointer to delete the <see cref="DataStorage"/> file
    /// </summary>
    [ContextMenu("Eliminas el archivo")] public void _Delete() => Delete();

    /// <summary>
    /// Sync with saved changes
    /// </summary>
    public static void Sync(Action<SavedData> callback, bool condition)
    {
        if (condition)
        {
            OnSyncSaved += callback;
        }
        else OnSyncSaved -= callback;
        
    }
    
    #region LocalData
    /// <summary>
    /// Get the information of <seealso cref="localData"/>
    /// </summary>
    public static LocalData GetLocal => _.localData;
    /// <summary>
    /// Set the information of <paramref name="newLocalData"/>  in <seealso cref="localData"/>
    /// </summary>
    public static void SetLocal(LocalData newLocalData) => _.localData = newLocalData;
    #endregion
    #endregion
}
#endregion
#region DataStorage y SavedData y LocalData
namespace StorageData
{
    /// <summary>
    /// Encargado de hacer que, con un constructor se agreguen los nuevos valores
    /// <para>Dependencias => <seealso cref="SavedData"/></para>
    /// </summary>
    [System.Serializable]
    public class DataStorage
    {
        //aquí se vuelve a colocar los datos puestos debajo...
        public SavedData savedData = new SavedData();
        //Con esto podremos guardar los datos de datapass a DataStorage
        public DataStorage(SavedData savedData) => this.savedData = savedData;
    }
}
/// <summary>
/// Este es el modelo de datos que vamos a guardar y manejar
/// para los archivos que se crean... Estos datos internos pueden cambiar para los proyectos...
/// <para>
///     Aquí almacenamos los datos internos del juego
/// </para>
/// </summary>
[System.Serializable]
public struct SavedData{


}
/// <summary>
/// Where the local information exist
/// </summary>
[System.Serializable]
public struct LocalData{

    public Transform tr_parent_bullet;

    [SerializeField] private Material mat_white;
    [SerializeField] private Material mat_black;
    [Space]
    [SerializeField] private Material mat_part_white;
    [SerializeField] private Material mat_part_black;

    public Material this[TypeData type] { get
        {
            Material mat = default;
            switch (type)
            {
                case TypeData.White:
                    mat = mat_white;
                    break;
                case TypeData.Black:
                    mat = mat_black;
                    break;
            }
            return mat;
        }
    }



    public Material GetParticle(TypeData type)
    {
        Material mat = default;
        switch (type)
        {
            case TypeData.White:
                mat = mat_part_white;
                break;
            case TypeData.Black:
                mat = mat_part_black;
                break;
        }
        return mat;
    }
    
}
#endregion