var dotNetHelper;
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

export function initSearch(_dotNetHelper) {
    dotNetHelper = _dotNetHelper;
    $('.blazored-typeahead__input-multiselect-wrapper').mutationSummary("connect", updatedQuery, [{
        all: true
    }]);
}

function updatedQuery(summaries) {
    dotNetHelper.invokeMethodAsync('UpdatedQuery');
}

