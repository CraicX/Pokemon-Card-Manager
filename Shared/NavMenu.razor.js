export function ToggleMenu(elo) {
    elo = '#' + elo;

    if ($(elo).hasClass('collapse')) {

        if ($(elo).parent().hasClass('menuCol')) {

            $('.menuCol').find('ul').addClass('collapse');
            $('.menuCol').find('i').removeClass('bx-chevron-down');
            $('.menuCol').find('i').addClass('bx-chevron-left');

        }

        $(elo).parent().find('i').removeClass('bx-chevron-left');
        $(elo).parent().find('i').addClass('bx-chevron-down');
        $(elo).removeClass('collapse');

    }
    else {

        $(elo).addClass('collapse');
        $(elo).parent().find('i').removeClass('bx-chevron-down');
        $(elo).parent().find('i').addClass('bx-chevron-left');

    }

}


// toggle menu button

export function ToggleSidebar() {

    if ($(".wrapper").hasClass("toggled")) {
        // unpin sidebar when hovered
        $(".wrapper").removeClass("toggled");
        $(".sidebar-wrapper").off("mouseleave");
        $(".sidebar-wrapper").off("mouseenter");
    } else {
        $(".wrapper").addClass("toggled");
        $(".sidebar-wrapper").on('mouseenter', function () {
            $(".wrapper").addClass("sidebar-hovered");
        });
        $(".sidebar-wrapper").on(' mouseleave', function () {
            $(".wrapper").removeClass("sidebar-hovered");
        });

    }

}