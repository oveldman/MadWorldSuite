## Get Started
Add the following project reference in your blazor project:
```bash
dotnet add reference ../MadWorld.ExternPackages.Monaco/MadWorld.ExternPackages.Monaco.csproj
```

Add the following html code in the head section of your index.html file:
```html
<link href="_content/MadWorld.ExternPackages.Monaco/lib/monaco-editor/min/vs/editor/editor.main.css" rel="stylesheet" />
```
Add the following html code in the script section of your index.html file:
```html
<script src="_content/MadWorld.ExternPackages.Monaco/lib/monaco-editor/min/vs/loader.js"></script>
<script>require.config({ paths: { 'vs': '_content/MadWorld.ExternPackages.Monaco/lib/monaco-editor/min/vs' } });</script>
```
## Using
Add the following code in your razor component:
```html
<MonacoEditor />
```