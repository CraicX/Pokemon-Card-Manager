
export function initFolderSort() {
    $('.folderGroup').nestedSortable({
        listType: 'ul',
        maxLevels: 3,
        forcePlaceholderSize: true,
        isTree: false,
        placeholder: 'placeholder',
        relocate: function (e, ui) {

            DotNet.invokeMethodAsync('PokeCardManager', 'FolderSorted', JSON.stringify(e.stuff));

        }
        
    });
        
}

export function disableFolderSort() {
    $('.folderGroup').nestedSortable('disable');
}

export function runFolderSort() {
    $('.pokeFolder').each(function () {
        
        if ($(this).data('parent') != 0)
        {
            let parent = $('.pokeFolder[data-folder-id="' + $(this).data('parent-id') + '"]');

            $(this).appendTo(parent.find('div').first());
        }
    });

}

export function toggleFolder(folderType) 
{
    $('.folderGroup[data-folder-type="' + folderType + '"]').toggle();

    let collapsed = false;

    if ($('.folderGroup[data-folder-type="' + folderType + '"]').css('display') == 'none' ) {

        $('.bx[data-folder-type="' + folderType + '"]').removeClass('bx-minus').addClass('bx-plus');

    } else {

        $('.bx[data-folder-type="' + folderType + '"]').removeClass('bx-plus').addClass('bx-minus');

        collapsed = true;

    }

    let tfObj = {
        'folderType': folderType,
        'collapsed': collapsed,
    };

    DotNet.invokeMethodAsync('PokeCardManager', 'FolderGroupCollapsed', folderType, collapsed);
}