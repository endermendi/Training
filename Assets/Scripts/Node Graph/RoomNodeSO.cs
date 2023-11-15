using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RoomNodeSO : ScriptableObject
{
    [HideInInspector] public string id;
    [HideInInspector] public List<string> parentRoomNodeIDList = new List<string>();
    [HideInInspector] public List<string> childRoomNodeIDList = new List<string>();
    [HideInInspector] public RoomNodeGraphSO roomNodeGRaph;
    public RoomNodeTypeSO roomNodeType;
    [HideInInspector] public RoomNodeTypeLĘstSO roomNodeTypeList;

    #region Editor Code

#if UNITY_EDITOR

    [HideInInspector]public Rect rect;

    public void Initialise(Rect rect, RoomNodeGraphSO nodeGraph, RoomNodeTypeSO roomNodeType)
    {
        this.rect = rect;

        this.id = System.Guid.NewGuid().ToString();

        this.name = "RoomNode";

        this.roomNodeGRaph = nodeGraph;

        this.roomNodeType = roomNodeType;

        roomNodeTypeList = GameResources.Instance.roomNodeTypeList;
    }

    public void Draw(GUIStyle nodeStyle)
    {
        GUILayout.BeginArea(rect, nodeStyle);

        EditorGUI.BeginChangeCheck();

        int selected = roomNodeTypeList.list.FindIndex(x => x == roomNodeType);

        int selection = EditorGUILayout.Popup("", selected, GetRoomNodeTypesToDisplay());

        roomNodeType = roomNodeTypeList.list[selection];
        
        if (EditorGUI.EndChangeCheck())
            EditorUtility.SetDirty(this);

        GUILayout.EndArea();
    }
    public string[] GetRoomNodeTypesToDisplay()
    {
        string[] roomArray = new string[roomNodeTypeList.list.Count];

        for (int i = 0; i < roomNodeTypeList.list.Count; i++)
        {
            if (roomNodeTypeList.list[i].displayInNodeGraphEditor)
            {
                roomArray[i] = roomNodeTypeList.list[i].roomNodeTypeName;
            }
        }

        return roomArray;
    }

#endif
    #endregion
}
