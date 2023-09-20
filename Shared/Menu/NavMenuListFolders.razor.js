

export function initFolderSort() {
    $('.folderGroup').nestedSortable({
        listType: 'ul',
        maxLevels: 3,
        forcePlaceholderSize: true,
        isTree: false,
        placeholder: 'placeholder',
        relocate: function (e, ui) {

            console.info(e.stuff);
            DotNet.invokeMethodAsync('PokeCardManager', 'FolderSorted', JSON.stringify(e.stuff));

        }
        
    });
        
}

export function runFolderSort() {
    $('.pokeFolder').each(function () {
        
        if ($(this).data('parent') != 0)
        {
            let parent = $('.pokeFolder[data-folderid="' + $(this).data('parent') + '"]');
            console.info("parent: ", parent);
            //parent.hide();
            //if (!parent.find('ul')) parent.append('<ul></ul>');
            //if (!parent.find('ul')) parent.hide();

            $(this).appendTo(parent.find('div').first());
        }
    });

}
