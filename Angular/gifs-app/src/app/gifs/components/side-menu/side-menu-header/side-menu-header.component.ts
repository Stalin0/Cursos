import { Component, OnInit } from "@angular/core";
import { environment } from "@environments/environment";
// import { environment } from '../../../../../environments/environment';

@Component({
  selector: "gifs-side-menu-header",
  templateUrl: "./side-menu-header.component.html",
  styleUrls: [],
})
export default class SideMenuHeaderComponent {
  envs = environment;
}
