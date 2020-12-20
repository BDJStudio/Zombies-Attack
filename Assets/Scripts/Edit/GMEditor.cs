#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(DataBase))]
public class GMEditor : Editor
{
    private DataBase db;

    public void OnEnable()
    {
        db = (DataBase)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (db.mobs.Count > 0)
        {
            foreach (Item mobs in db.mobs)
            {
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.BeginHorizontal();
                
                EditorGUILayout.BeginVertical();

                mobs.nameMob = EditorGUILayout.TextField("Имя моба", mobs.nameMob);
                mobs.prefab = (GameObject)EditorGUILayout.ObjectField("Прифаб", mobs.prefab, typeof(GameObject), true);

                mobs.chancePer = EditorGUILayout.IntField("Шанс спавна", mobs.chancePer);
                mobs.countHP = EditorGUILayout.FloatField("Здоровье", mobs.countHP);
                mobs.speed = EditorGUILayout.FloatField("Скорость", mobs.speed);
                mobs.countDam = EditorGUILayout.FloatField("Урон", mobs.countDam);
                mobs.deadPoints = EditorGUILayout.IntField("Очки з убийство", mobs.deadPoints);
                
                EditorGUILayout.EndVertical();
                
                if (GUILayout.Button("Delete", GUILayout.Width(60), GUILayout.Height(60)))
                {
                    db.mobs.Remove(mobs);
                    break;
                }
                
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
            }
        }
        else EditorGUILayout.LabelField("no elements");

        if (GUILayout.Button("Add", GUILayout.Height(30)))
            db.mobs.Add(new Item());

        if (GUI.changed)
            SetObjectDirty(db.gameObject);
    }
    
    public static void SetObjectDirty(GameObject obj)
    {
        EditorUtility.SetDirty(obj);
        EditorSceneManager.MarkSceneDirty(obj.scene);
    }
}
#endif