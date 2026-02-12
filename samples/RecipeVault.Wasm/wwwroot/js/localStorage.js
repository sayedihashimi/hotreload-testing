// localStorage.js - Browser storage and utilities

// Download file helper
window.downloadFile = function (fileName, content) {
    const blob = new Blob([content], { type: 'application/json' });
    const url = URL.createObjectURL(blob);
    const link = document.createElement('a');
    link.href = url;
    link.download = fileName;
    link.click();
    URL.revokeObjectURL(url);
};

// Online status listener
window.registerOnlineStatusListener = function (dotNetObject) {
    window.addEventListener('online', () => {
        dotNetObject.invokeMethodAsync('UpdateOnlineStatus', true);
    });
    
    window.addEventListener('offline', () => {
        dotNetObject.invokeMethodAsync('UpdateOnlineStatus', false);
    });
    
    // Initialize with current status
    dotNetObject.invokeMethodAsync('UpdateOnlineStatus', navigator.onLine);
};
