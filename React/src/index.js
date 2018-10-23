import { GoodBoyForm } from './components';

// in order to use these components in a non-SPA web app, they need to be declared on global or window
// then the bundle.js should be included in the <head> tag of the non-SPA web app
const global = window || global;
global.GoodBoyForm = GoodBoyForm;
