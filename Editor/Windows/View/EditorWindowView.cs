using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEditor;
using Bolt.Addons.Libraries.Humility;
using System;
using UnityEngine.UIElements;

namespace Bolt.Addons.Community.Utility.Editor
{
    [IncludeInSettings(true)]
    [Serializable]
    public class EditorWindowView : EditorWindow, IMacro
    {
        public EditorWindowAsset asset;

        [MenuItem("Window/Community Addons/New Editor Window View")]
        public static void OpenNew()
        {
            EditorWindowView window = CreateInstance<EditorWindowView>();
            window.titleContent = new GUIContent("Editor Window View");
            window.Show();
        }

        public IGraph childGraph => asset == null ? graph : asset.graph;

        public bool isSerializationRoot => true;

        public UnityEngine.Object serializedObject => asset;

        public IEnumerable<object> aotStubs => graph.aotStubs;

        [SerializeReference]
        private IGraph _graph = new FlowGraph();
        public IGraph graph { get => asset == null ? _graph : asset.graph; set { if (asset == null) { _graph = value; } else { asset.graph = (FlowGraph)value; } } }

        private GraphReference reference;

        [SerializeField] 
        private bool isAssetReference;

        [Inspectable][SerializeField]
        public CustomVariables variables = new CustomVariables();

        public VisualElement container;

        public Event e { get; private set; }

        private bool firstPass = true;

        private void OnHeaderGUI() 
        {
            e = Event.current;

            HUMEditor.Horizontal().Box(HUMEditorColor.DefaultEditorBackground.Darken(0.1f), Color.black, new RectOffset(2, 2, 2, 2), new RectOffset(2, 2, 2, 2), () =>
            {
                HUMEditor.Disabled(asset == null, () =>
                {
                    if (GUILayout.Button("Variables", GUILayout.Width(100)))
                    {
                        EditorWindowVariables.Open(new Rect(GUIUtility.GUIToScreenPoint(new Vector2(e.mousePosition.x, e.mousePosition.y)), new Vector2(220, 300)), asset, this);
                    }

                    if (GUILayout.Button("Edit Graph", GUILayout.Width(120)))
                    {
                        GraphWindow.OpenActive(GetReference() as GraphReference);
                    }
                });

                HUMEditor.Changed(() => 
                {
                    asset = (EditorWindowAsset)EditorGUILayout.ObjectField(asset, typeof(EditorWindowAsset), false);
                }, ()=> 
                {
                    if (asset == null)
                    {
                        variables.Clear();
                    }
                    else
                    {
                        if (!firstPass) variables.Clear();
                        variables.CopyFrom(asset.variables, true);
                    }
                });

                firstPass = false;
            }, GUILayout.Height(24));
        }

        private void OnContainerGUI()
        {
            GetReference().AsReference().TriggerEventHandler<EditorWindowEventArgs>((hook) => { return hook == "EditorWindow_OnGUI"; }, new EditorWindowEventArgs(this), (p) => true, true);
        }

        public GraphPointer GetReference()
        {
            return GraphReference.New(asset == null ? this : (IGraphRoot)asset, true); ;
        }

        public IGraph DefaultGraph()
        {
            return new FlowGraph();
        }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
        }

        private void OnDestroy()
        {
            variables.onVariablesChanged -= OnVariablesChanged;
            GetReference().AsReference().TriggerEventHandler<EditorWindowEventArgs>((hook) => { return hook == "EditorWindow_OnDestroy"; }, new EditorWindowEventArgs(this), (p) => true, true);
        }
        
        private void OnDisable()
        {
            variables.onVariablesChanged -= OnVariablesChanged;
            GetReference().AsReference().TriggerEventHandler<EditorWindowEventArgs>((hook) => { return hook == "EditorWindow_OnDisable"; }, new EditorWindowEventArgs(this), (p) => true, true);

            var windows = Resources.FindObjectsOfTypeAll<EditorWindowVariables>();
            for (int i = 0; i < windows.Length; i++)
            {
                windows[i].Close();
                UnityEngine.Object.DestroyImmediate(windows[i]);
            } 
        }

        private void OnEnable()
        {
            var header = new IMGUIContainer(OnHeaderGUI);
            header.style.height = 27;
            container = new IMGUIContainer(OnContainerGUI);
            container.style.flexGrow = 1;

            rootVisualElement.Add(header);
            rootVisualElement.Add(container);

            if (asset != null) variables.CopyFrom(asset.variables);
            variables.onVariablesChanged += OnVariablesChanged;

            GetReference().AsReference().TriggerEventHandler<EditorWindowEventArgs>((hook) => { return hook == "EditorWindow_OnEnable"; }, new EditorWindowEventArgs(this), (p) => true, true);
        }

        private void OnFocus()
        {
            GetReference().AsReference().TriggerEventHandler<EditorWindowEventArgs>((hook) => { return hook == "EditorWindow_OnFocus"; }, new EditorWindowEventArgs(this), (p) => true, true);
        }

        private void OnLostFocus()
        {
            GetReference().AsReference().TriggerEventHandler<EditorWindowEventArgs>((hook) => { return hook == "EditorWindow_OnLostFocus"; }, new EditorWindowEventArgs(this), (p) => true, true);
        }

        private void OnVariablesChanged() 
        {
            if (asset != null)
            {
                variables.CopyFrom(asset.variables);
            }
            else
            {
                variables.Clear();
            }
        }
    }
}