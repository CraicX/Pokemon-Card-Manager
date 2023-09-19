export function validateFolderName() {

    if ($('#folderName').val() != '' && $('#folderType').find(":selected").val() != '') {
        $('#folderAdd').removeClass('disabled');
    } else {
        $('#folderAdd').addClass('disabled');
    }
}

export function closeFolderModal() {
    $('#folderName').val('');
    $('#folderType').val('');
    $('#folderAdd').addClass('disabled');
    $('#createFolderModal').modal('hide');
}