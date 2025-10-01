import { Component, input, OnInit } from "@angular/core";

@Component({
  selector: "gif-list-item",
  templateUrl: "./gif-list-item.component.html",
  styleUrls: [],
})
export default class GifListItemComponent {
  imageUrl = input.required<string>();
}
