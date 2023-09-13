export function validateFolderName() {

    if ($('#folderName').val() != '' && $('#folderType').find(":selected").val() != '') {
        $('#folderAdd').removeClass('disabled');
    } else {
        $('#folderAdd').addClass('disabled');
    }

}