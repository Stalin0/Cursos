import { Component, OnInit } from "@angular/core";
import GifListItemComponent from "./gif-list-item/gif-list-item.component";

@Component({
  selector: "gif-list",
  templateUrl: "./gif-list.component.html",
  styleUrls: [],
  imports: [GifListItemComponent],
})
export default class GifListComponent {}
