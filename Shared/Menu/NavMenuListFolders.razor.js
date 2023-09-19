

export function initFolderDrop() {
    $('#mm_folders .pokeFolder').draggable({
        handle: ' > a',
        opacity: .8,
        addClasses: false,
        helper: 'clone',
        zIndex: 100,
        create: function (e, ui) {
            $(this).draggable('option', 'scope', $(this).data('foldertype'));
        }
    });

    $('#mm_folders a, #mm_folders .dropzone').droppable({
        tolerance: 'pointer',
        create(e, ui) {
            $(this).droppable('option', 'scope', $(this).parent().data('foldertype'));
        },
        drop: function (e, ui) {
            var li = $(this).parent();
            var child = !$(this).hasClass('dropzone');
            if (child && li.children('ul').length == 0) {
                li.append('<ul/>');
            }
            if (child) {
                li.children('ul').append(ui.draggable);
            }
            else {
                li.before(ui.draggable);
            }
            li.find('a,.dropzone').css({ backgroundColor: '', borderColor: '' });
        },
        over: function () {

            $(this).filter('a').css({ backgroundColor: '#ccc' });
            $(this).filter('.dropzone').css({ borderColor: '#aaa' });
        },
        out: function () {
            $(this).filter('a').css({ backgroundColor: '' });
            $(this).filter('.dropzone').css({ borderColor: '' });
        }
    });

    $('#mm_folders .folderTitle').droppable({
        tolerance: 'pointer',
        create(e, ui) {
            $(this).droppable('option', 'scope', $(this).data('foldertype'));
        },
        drop: function (e, ui) {
            $("ul[data-foldertype='" + ui.draggable.data('foldertype') + "']").append(ui.draggable);

            $(this).css({ backgroundColor: '', borderColor: '' });
        },
        over: function () {

            $(this).css({ backgroundColor: '#ccc' });
            $(this).css({ borderColor: '#aaa' });
        },
        out: function () {
            $(this).css({ backgroundColor: '' });
            $(this).css({ borderColor: '' });
        }
    });

}