function clickFileInput() {
    document.getElementById('fileInput').click();
}

function getDroppedFiles(dataTransfer) {
    return Array.from(dataTransfer.files).map(file => ({
        name: file.name,
        size: file.size,
        lastModified: file.lastModified,
        contentType: file.type
    }));
}