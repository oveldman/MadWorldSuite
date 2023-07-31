const emptyValue = "";
let currentEditor = null;

export function init(editorId) {
    currentEditor = window.monaco.editor.create(document.getElementById(editorId), {
        emptyValue,
        language: "javascript",
        automaticLayout: true,
    });
}