//  Main JS Funcs
var cBoundAsync;
var projObj;


function validateFolderName() {
    if ($('#folderName').val() != '' && $('#folderType').find(":selected").val() != '') {
        $('#folderAdd').removeClass('disabled');
    } else {
        $('#folderAdd').addClass('disabled');
    }
}

const IconMap = {
    "success": "bx bx-check-circle",
    "error": "bx bx-x-circle",
    "warning": "bx bx-error",
    "info": "bx bx-info-circle",
    "primary": "bx bx-info-circle",
    "secondary": "bx bx-info-circle",
    "default": "bx bx-info-circle",
};

async function showAlert(msg, type = 'success', options = '{}') {

    let defaults = {
        sound: false,
        rounded: true,
        closeButton: false,
        position: 'bottom right',
        showClass: 'fadeInScale',
        hideClass: 'zoomOut',
        icon: IconMap[type],
        delayIndicator: false,
        msg: msg,
        size: "normal"
    };

    let alertOptions = await Object.assign(defaults, JSON.parse(options));

    await Lobibox.notify(type, alertOptions);

    return true;
};
