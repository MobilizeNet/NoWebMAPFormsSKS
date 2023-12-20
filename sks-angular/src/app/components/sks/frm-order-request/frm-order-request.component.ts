import { Component, ChangeDetectorRef, ElementRef
, Output, Renderer2, ViewEncapsulation} from "@angular/core";
import { EventData, dataTransfer} from "@mobilize/base-components";
import { FormComponent} from "@mobilize/winforms-components";
import { WebMapService} from "@mobilize/angularclient";
import { ActivatedRoute, Router } from "@angular/router";
//import { ActivatedRoute } from "@angular/router";
@Component({
   selector : "sks-frm-order-request",
   styleUrls : ["./frm-order-request.component.css"],
   templateUrl : "./frm-order-request.component.html",
   encapsulation : ViewEncapsulation.None
})
@dataTransfer(["frmSKSfrmOrderRequest"])
export class frmOrderRequestComponent extends FormComponent {
   protected webServices : WebMapService;

   constructor (wmservice : WebMapService,
   changeDetector : ChangeDetectorRef,render2 : Renderer2,elem : ElementRef, private router: Router) {
      super(wmservice,changeDetector,render2,elem);
   }

   btnNoWebMapClickHandler(event: any): void {
      const selectedCustomersIds: string[] = [];
      const selectedIndices: number[] = this.model.lvCustomers?.SelectedIndices;
      selectedIndices?.forEach((index) => {
         selectedCustomersIds.push(this.model.lvCustomers.itemData[index].Customer_ID);
      });
      this.router.navigate(['/no-webmap-form'], {
         state: {
            SelectedCustomersIds: selectedCustomersIds
         }
      });
   }

   
}