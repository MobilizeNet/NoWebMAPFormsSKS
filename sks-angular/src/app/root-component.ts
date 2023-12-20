import { Component } from '@angular/core';

import { WebMapService } from '@mobilize/angularclient';

@Component({
  selector: 'root-app',
  templateUrl: './root-component.html',
  styleUrls: ['./app.component.css']
})
export class RootComponent {


  constructor( private webmapService: WebMapService) {

    webmapService.init();
     }
}
