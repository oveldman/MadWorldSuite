const emptyValue = "";
let currentEditor = null;

export function init(editorId, monacoSettings) {
    let settings = JSON.parse(monacoSettings);
    
    currentEditor = window.monaco.editor.create(document.getElementById(editorId), {
        emptyValue,
        language: settings.Language,
        automaticLayout: true,
        theme: "vs-dark",
    });
}

export function setValue(value) {
    currentEditor.getModel().setValue(value);
}