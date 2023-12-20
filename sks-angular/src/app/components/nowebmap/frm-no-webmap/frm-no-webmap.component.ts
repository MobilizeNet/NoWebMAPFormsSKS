import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Input } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { WebMapUtils, WebMapService } from '@mobilize/angularclient';
import { DataGridViewComponent, ClientCustomUpdateService, EventData } from '@mobilize/winforms-components'


@Component({
  selector: "frm-nowebmap",
  templateUrl: './frm-no-webmap.component.html'
})
export class frmNoWebMapComponent implements OnInit {

  private state: any;

  private selectedCustomersIds: string[] = [];

  constructor(private _router: Router, private http: HttpClient, private wmService: WebMapService,
    private refreshComponentService: ClientCustomUpdateService) {
      // You can only get the state from the constructor.
      this.state = this._router.getCurrentNavigation()?.extras?.state;
  }

  ngOnInit(): void {
    this.loadData();
    this.getStateParams();
  }

  @Input()
  grid: DataGridViewComponent;

  name = '';

  lastName = '';

  customerId = null;

  public opened = true;
  public dataSaved = false;

  public customer: any;

  public close(): void {
    this.opened = false;
    this._router.navigate(['/']);
  }

  public open(): void {
    this.opened = true;
  }

  public submit(): void {
    this.dataSaved = true;
    // This is needed to send the correct headers to connect to the webmap server
    let headers = WebMapUtils.getWebApiHeaders();
    // This to avoid the queue on the server
    delete headers['WM-API'];
    let params = new HttpParams();
    params = params.append('id', this.customerId);
    params =params.append('name', this.name);
    params = params.append('lastName', this.lastName);
    this.http.post('api/NoWebMapController/UpdateCustomer', {}, {headers: headers, params: params}).subscribe(() => {
      this.close();
      const event = new EventData({}, '123');
      event['args'] = {};
      event.args['name'] = 'frmOrderRequest';

      //Custom args
      const currLastName = this.customer.Customers.Table[0].ContactLastName;
      // If the last name changed 
      if (this.lastName !== currLastName) {
        event.args['customArgs'] = {
          LastNameChanged: true,
          CurrentLastName: currLastName,
          NewLastName: this.lastName,
          Id: this.customerId
        };
      }
      this.refreshComponentService.notifyRefreshComponentData(event);
    });
  }

  public loadData(): void {
    // This is needed to send the correct headers to connect to the webmap server
    let headers = WebMapUtils.getWebApiHeaders();
    // This to avoid the queue on the server
    delete headers['WM-API'];
    this.http.get('api/NoWebMapController/GetCustomerInfo', {headers: headers}).subscribe((data) => {
      this.customer = data;
      this.name = this.customer.Customers.Table[0].ContactFirstName;
      this.lastName = this.customer.Customers.Table[0].ContactLastName;
      this.customerId = this.customer.Customers.Table[0].CustomerID;
    });
  }

  getStateParams(): void {
    this.selectedCustomersIds = this.state.SelectedCustomersIds;
  }

  isDisplayedUserSelected(): boolean {
    return this.selectedCustomersIds.includes(this.customerId?.toString());
  }
};
