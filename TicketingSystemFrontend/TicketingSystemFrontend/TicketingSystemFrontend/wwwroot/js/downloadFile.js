window.downloadFileFromBytes = (fileName, base64) => {
    const link = document.createElement('a');
    link.href = "data:application/octet-stream;base64," + base64;
    link.download = fileName;
    link.click();
};
