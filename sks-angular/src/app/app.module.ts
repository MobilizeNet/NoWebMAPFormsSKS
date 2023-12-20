import { BrowserModule } from '@angular/platform-browser';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA, NgZone, ChangeDetectorRef} from '@angular/core';
import { AppComponent } from './app.component';
import { BaseComponentsModule } from '@mobilize/base-components';
import { WebMapKendoModule, ClientCustomUpdateService } from '@mobilize/winforms-components';
import { WebMapService, WebMapModule } from '@mobilize/angularclient';
import { SKSModule } from './sks.module';
import { RouterModule } from '@angular/router';
import { frmNoWebMapComponent } from './components/nowebmap/frm-no-webmap/frm-no-webmap.component';
import { RootComponent } from './root-component';

import { InputsModule } from '@progress/kendo-angular-inputs';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { WindowModule } from '@progress/kendo-angular-dialog';
import { ButtonsModule } from '@progress/kendo-angular-buttons';

@NgModule({
  declarations: [
    AppComponent,
    frmNoWebMapComponent,
    RootComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    BaseComponentsModule,
    WebMapKendoModule,
    WebMapModule,
    SKSModule,
    ButtonsModule,
    InputsModule,
    WindowModule,
    RouterModule.forRoot([
      //{ path: '', redirectTo: 'mainapp', pathMatch: "full"},
      // {path: 'mainapp', component: AppComponent},
      {path: 'no-webmap-form', component: frmNoWebMapComponent}

    ])
  ],
  providers: [WebMapService, ClientCustomUpdateService],
  bootstrap: [AppComponent],
  schemas: [ CUSTOM_ELEMENTS_SCHEMA ]
})
export class AppModule { }

