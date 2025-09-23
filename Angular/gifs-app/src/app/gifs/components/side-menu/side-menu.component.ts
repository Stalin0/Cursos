import { Component, OnInit } from "@angular/core";
import SideMenuHeaderComponent from "./side-menu-header/side-menu-header.component";
import SideMenuOptionsComponent from "./side-menu-options/side-menu-options.component";

@Component({
  selector: "gifs-side-menu",
  templateUrl: "./side-menu.component.html",
  styleUrls: [],
  imports: [SideMenuHeaderComponent, SideMenuOptionsComponent],
})
export default class SideMenuComponent {}
