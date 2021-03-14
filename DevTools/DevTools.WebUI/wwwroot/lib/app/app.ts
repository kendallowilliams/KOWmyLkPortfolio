import DevTools from './devtools/devtools';

export default class App {
    private devTools: DevTools;

    constructor() {
        this.initialize();
        this.devTools = new DevTools();
    }

    private initialize(): void {
    }
}