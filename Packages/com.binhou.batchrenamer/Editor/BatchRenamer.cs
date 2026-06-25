using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class BatchRenamer : EditorWindow
{
    private enum IndexMode { Numeric, Alphabetic }
    private enum SortMode  { SelectionOrder, AlphaAscending, AlphaDescending }

    private string    prefix       = "Object";
    private string    separator    = "_";
    private IndexMode indexMode    = IndexMode.Numeric;
    private int       startIndex   = 1;
    private int       digitPadding = 0;
    private SortMode  sortMode     = SortMode.SelectionOrder;

    private Vector2 scrollPos;
    private List<(Object obj, string newName)> previewList = new();
    private GUIStyle headerStyle;
    private GUIStyle oldNameStyle;
    private GUIStyle newNameStyle;

    [MenuItem("Tools/Batch Renamer %#R")]
    public static void Open()
    {
        var win = GetWindow<BatchRenamer>("Batch Renamer");
        win.minSize = new Vector2(420, 500);
        win.Show();
    }

    private void OnEnable()  => Selection.selectionChanged += Repaint;
    private void OnDisable() => Selection.selectionChanged -= Repaint;

    private void InitStyles()
    {
        if (headerStyle != null) return;

        headerStyle = new GUIStyle(EditorStyles.boldLabel)
        {
            fontSize = 13,
            margin   = new RectOffset(0, 0, 6, 6)
        };

        oldNameStyle = new GUIStyle(EditorStyles.label)
        {
            normal = { textColor = new Color(0.6f, 0.6f, 0.6f) }
        };

        newNameStyle = new GUIStyle(EditorStyles.label)
        {
            normal = { textColor = new Color(0.3f, 0.85f, 0.5f) }
        };
    }

    private void OnGUI()
    {
        InitStyles();
        BuildPreview();

        EditorGUILayout.Space(8);
        EditorGUILayout.LabelField("Naming Settings", headerStyle);
        DrawSettings();

        EditorGUILayout.Space(6);
        EditorGUILayout.LabelField("Preview", headerStyle);
        DrawPreview();

        EditorGUILayout.Space(6);
        DrawActions();
    }

    private void DrawSettings()
    {
        EditorGUI.BeginChangeCheck();

        using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
        {
            prefix    = EditorGUILayout.TextField(new GUIContent("Prefix",    "e.g. Monster, Enemy, NPC"), prefix);
            separator = EditorGUILayout.TextField(new GUIContent("Separator", "Default: _"),               separator);
            indexMode = (IndexMode)EditorGUILayout.EnumPopup(new GUIContent("Index Mode", "Numeric: 1,2,3 / Alphabetic: A,B,C"), indexMode);

            if (indexMode == IndexMode.Numeric)
            {
                startIndex   = EditorGUILayout.IntField(new GUIContent("Start Index",   "Starting number"), startIndex);
                digitPadding = EditorGUILayout.IntSlider(new GUIContent("Digit Padding", "0=off / 2=01 / 3=001"), digitPadding, 0, 4);
            }

            sortMode = (SortMode)EditorGUILayout.EnumPopup(new GUIContent("Sort Order", "Order in which indices are assigned"), sortMode);
        }

        if (EditorGUI.EndChangeCheck())
            BuildPreview();
    }

    private void DrawPreview()
    {
        if (previewList.Count == 0)
        {
            EditorGUILayout.HelpBox("Select objects in the Project or Hierarchy window.", MessageType.Info);
            return;
        }

        EditorGUILayout.LabelField($"{previewList.Count} object(s) selected", EditorStyles.miniLabel);
        EditorGUILayout.Space(2);

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.MaxHeight(260));

        foreach (var (obj, newName) in previewList)
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField(obj.name, oldNameStyle, GUILayout.Width(160));
                EditorGUILayout.LabelField("->", GUILayout.Width(22));
                EditorGUILayout.LabelField(newName, newNameStyle);
            }
        }

        EditorGUILayout.EndScrollView();
    }

    private void DrawActions()
    {
        if (GUILayout.Button("Rename", GUILayout.Height(36)))
            ExecuteRename();

        EditorGUILayout.Space(4);
        EditorGUILayout.LabelField("Shortcut: Ctrl+Shift+R  (Mac: Cmd+Shift+R)", EditorStyles.centeredGreyMiniLabel);
    }

    private void BuildPreview()
    {
        previewList.Clear();

        var objects = GetSortedSelection();
        int numIdx  = startIndex;
        int alphaIdx = 0;

        foreach (var obj in objects)
        {
            string index = indexMode == IndexMode.Alphabetic
                ? GetAlphaIndex(alphaIdx)
                : (digitPadding > 0 ? numIdx.ToString($"D{digitPadding}") : numIdx.ToString());

            string newName = string.IsNullOrEmpty(prefix)
                ? index
                : $"{prefix}{separator}{index}";

            previewList.Add((obj, newName));

            numIdx++;
            alphaIdx++;
        }
    }

    private void ExecuteRename()
    {
        if (previewList.Count == 0)
        {
            EditorUtility.DisplayDialog("Batch Renamer", "No objects selected.", "OK");
            return;
        }

        bool confirm = EditorUtility.DisplayDialog(
            "Confirm Rename",
            $"Rename {previewList.Count} object(s)? This can be undone with Ctrl+Z.",
            "Rename", "Cancel");

        if (!confirm) return;

        Undo.RecordObjects(previewList.Select(p => p.obj).ToArray(), "Batch Rename");

        foreach (var (obj, newName) in previewList)
        {
            string assetPath = AssetDatabase.GetAssetPath(obj);
            if (!string.IsNullOrEmpty(assetPath))
                AssetDatabase.RenameAsset(assetPath, newName);
            else
                obj.name = newName;
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log($"[BatchRenamer] Renamed {previewList.Count} object(s).");
    }

    private List<Object> GetSortedSelection()
    {
        var list = Selection.objects.ToList();

        return sortMode switch
        {
            SortMode.AlphaAscending  => list.OrderBy(o => o.name).ToList(),
            SortMode.AlphaDescending => list.OrderByDescending(o => o.name).ToList(),
            _                        => list
        };
    }

    private static string GetAlphaIndex(int index)
    {
        string result = "";
        index++;
        while (index > 0)
        {
            index--;
            result = (char)('A' + index % 26) + result;
            index /= 26;
        }
        return result;
    }
}
