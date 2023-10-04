export function setTheme(theme) {

    if (theme == 'dark-theme') {
        $('html').removeClass('light-theme');
        $("#themeIcon").removeClass('bx-sun');
        $("#themeIcon").addClass('bx-moon');
    }
    else {
        $('html').removeClass('dark-theme')
        $("#themeIcon").removeClass('bx-moon');
        $("#themeIcon").addClass('bx-sun');
    }

    $('html').addClass(theme);

}

