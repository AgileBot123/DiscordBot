let storage = [];



function showStorage() {
    var h3 = document.getElementById('showStorage')
    h3 = storage;
}



function RemoveFromLocalStorage() {
    var settingsArray = document.getElementsByName('SettingsArray[]');

    for (var i = 0; i < settingsArray.length; i++) {
        storage.pop(i);
    }
}


function AddToLocalStorage() {

    var settingsArray = document.getElementsByName('SettingsArray[]');

    for (var i = 0; i < settingsArray.length; i++) {
        storage.push(i);
    }
}

