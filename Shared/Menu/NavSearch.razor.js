export function setTheme(theme) {

    if (theme == 'dark-theme') {
        $('html').removeClass('light-theme');
        $(".dark-mode-icon i").removeClass('bx-sun');
        $(".dark-mode-icon i").addClass('bx-moon');
    }
    else {
        $('html').removeClass('dark-theme')
        $(".dark-mode-icon i").removeClass('bx-moon');
        $(".dark-mode-icon i").addClass('bx-sun');
    }

    $('html').addClass(theme);

}

