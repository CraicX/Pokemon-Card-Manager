var dotNetHelper;
var dropDownPlugin = false;
var jqToolSet;
export function buildCardFolders() {



    if (!dropDownPlugin) {
        dropDownPlugin = true;


        (function ($bs) {
            const CLASS_NAME = 'has-child-dropdown-show';
            $bs.Dropdown.prototype.toggle = function (_orginal) {
                return function () {
                    document.querySelectorAll('.' + CLASS_NAME).forEach(function (e) {
                        e.classList.remove(CLASS_NAME);
                    });
                    let dd = this._element.closest('.dropdown').parentNode.closest('.dropdown');
                    for (; dd && dd !== document; dd = dd.parentNode.closest('.dropdown')) {
                        dd.classList.add(CLASS_NAME);
                    }
                    return _orginal.call(this);
                }
            }($bs.Dropdown.prototype.toggle);

            document.querySelectorAll('.dropdown').forEach(function (dd) {
                dd.addEventListener('hide.bs.dropdown', function (e) {
                    if (this.classList.contains(CLASS_NAME)) {
                        this.classList.remove(CLASS_NAME);
                        e.preventDefault();
                    }
                    e.stopPropagation(); // do not need pop in multi level mode
                });
            });

            // for hover
            document.querySelectorAll('.dropdown-hover, .dropdown-hover-all .dropdown').forEach(function (dd) {
                dd.addEventListener('mouseenter', function (e) {
                    let toggle = e.target.querySelector(':scope>[data-bs-toggle="dropdown"]');
                    if (toggle != null && !toggle.classList.contains('show')) {
                        $bs.Dropdown.getOrCreateInstance(toggle).toggle();
                        dd.classList.add(CLASS_NAME);
                        //$bs.Dropdown.clearMenus();
                    }
                });
                dd.addEventListener('mouseleave', function (e) {
                    let toggle = e.target.querySelector(':scope>[data-bs-toggle="dropdown"]');
                    if (toggle != null && toggle.classList.contains('show')) {
                        $bs.Dropdown.getOrCreateInstance(toggle).toggle();
                    }
                });
            });
        })(bootstrap);
    }

    $('.pokecard-tool-set .btn').off('click');
    $('.pokecard-tool-set .btn').on('click', function () {
        if ($(this).attr('name') == 'pts-tcg') {
            showAlert('Opening webpage to TCGPlayer!', 'info', '{ "size": "mini" }');
            window.open($(this).closest(".pokecard-info").data('tcg'));
        }
    });

}


export function folderSelected() {
    $('.pokecard-tool-set .show').removeClass('show');
    

}


