export function initCardEffects() {

    startCardEffects();

    const ps = new PerfectScrollbar('#search-container', {
        wheelSpeed: 1,
        wheelPropagation: true,
        minScrollbarLength: 20
    });

    ps.update();

    
}

export function updateResultCount(totalCount) {
    $('#searchText').text("Results: " + totalCount + " found");
}

