import BaseClass from "../assets/models/base-class";
import ScaffoldDbContextProfiles from './scaffold-db-context-profiles/scaffold-db-context-profiles';
import Settings from './settings/settings';

export default class DevTools extends BaseClass {
    private scaffoldDbContextProfiles: ScaffoldDbContextProfiles;
    private settings: Settings;

    constructor() {
        super();
        this.initialize();
    }

    private initialize(): void {
        this.scaffoldDbContextProfiles = new ScaffoldDbContextProfiles();
        this.settings = new Settings();
    }
}