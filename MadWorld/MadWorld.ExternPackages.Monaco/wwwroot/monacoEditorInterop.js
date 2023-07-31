const emptyValue = "";
let currentEditor = null;

export function init(editorId) {
    currentEditor = window.monaco.editor.create(document.getElementById(editorId), {
        emptyValue,
        language: "plaintext",
        automaticLayout: true,
        theme: "vs-dark",
    });
}

export function setValue(value) {
    currentEditor.getModel().setValue(value);
}