using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Dialog))]
public class DialogEditor : Editor {
	
	public SerializedProperty nodesProp;
	public SerializedObject targetObjectSO;
	public Dialog targetObject;
	
	
	public void  OnEnable (){
		// Setup the SerializedProperties
		
		targetObjectSO = new SerializedObject(target);
		nodesProp = targetObjectSO.FindProperty("nodes");
		targetObject = target as Dialog;
	}
	
	
	public override void OnInspectorGUI (){
		targetObjectSO.Update ();
		
		if(GUILayout.Button("Edit")) {
			DialogWindow.Open(targetObject, this);
		}
		
		
		
		//        targetObject.characterName = EditorGUILayout.TextField("Character Name:", targetObject.characterName);
		
		targetObject.filePath = EditorGUILayout.TextField("File:", targetObject.filePath);
		
		targetObject.nametagOffset = EditorGUILayout.Vector3Field("Nametag Position:", targetObject.nametagOffset);
		
		targetObject.foldoutTargets = EditorGUILayout.Foldout(targetObject.foldoutTargets, "Registered Targets");
		
		if(targetObject.foldoutTargets) {
			EditorGUI.indentLevel = 2;
			
			targetObject.registeredTargetsSize = EditorGUILayout.IntField("Size:",  targetObject.registeredTargetsSize);
			if(targetObject.registeredTargetsSize<2)
				targetObject.registeredTargetsSize = 2;
			
			if(targetObject.registeredTargets.Length != targetObject.registeredTargetsSize) {
				string[] newArray= new string[targetObject.registeredTargetsSize];
				for(int x = 0; x < targetObject.registeredTargetsSize; x++) {
					if(targetObject.registeredTargets.Length > x) {
						newArray[x] = targetObject.registeredTargets[x];
					} else {
						newArray[x] = "";
					}
				}
				targetObject.registeredTargets = newArray;
			}
			
			for(int y = 0; y < targetObject.registeredTargets.Length; y++) {
				//The first two slots are reserved for the player and the dialog's parent object
				//and will be disabled, so they the field can't be edited
				if(y<2)
					GUI.enabled = false;
				targetObject.registeredTargets[y] = EditorGUILayout.TextField("Target "+y, targetObject.registeredTargets[y]);
				if(y<2)
					GUI.enabled = true;
			}
			
		}
		
		if (GUI.changed) {
			EditorUtility.SetDirty (target);
			EditorUtility.SetDirty (targetObject);
		}
		
		targetObjectSO.ApplyModifiedProperties ();
	}
}


