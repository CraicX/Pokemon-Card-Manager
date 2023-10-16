export function InfiniteInit(component, observerTargetId) {

    window.Observer = {
        observer: null,
        lastFound: 0,
        Initialize: function (component, observerTargetId) {
            this.observer = new IntersectionObserver(e => {
                if (Math.floor(Date.now() / 1000) - this.lastFound > 1.5) {
                    this.lastFound = Math.floor(Date.now() / 1000);
                    component.invokeMethodAsync('OnIntersection');
                    this.lastFound = Math.floor(Date.now() / 1000);
                }
            });

            let element = document.getElementById(observerTargetId);
            if (element == null) throw new Error("The observable target was not found");
            this.observer.observe(element);
        }
    };

    Observer.Initialize(component, observerTargetId);

}