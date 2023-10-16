var ps;


export function initCardEffects() {

    startCardEffects();

    var container = document.getElementById('search-container');

    if (ps != null) ps.update(container);

    
}

export function addCardClick() {
    setCardClick();
}

export function parkToolSet() {
    $('.pokecard-tool-set').appendTo($('.toolset-parking'));
    $('.pokecard-tool-set').hide(); 
}

export function initPS() {
     ps = new PerfectScrollbar('#search-container', {
         wheelSpeed: 1,
         wheelPropagation: true,
         minScrollbarLength: 1
     });
}

export function updateResultCount(totalCount) {
    $('#searchText').text("Results: " + totalCount + " found");
}

export function hideSearching() {
    $('#searchSpinner').hide();
}

export function showSearching() {
    $('#searchSpinner').show();
}