import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import TopMenuComponent from '../../components/top-menu/top-menu.component';
@Component({
  selector: 'app-CountryLayout',
  templateUrl: './CountryLayout.component.html',
  imports: [TopMenuComponent, RouterOutlet],
})
export class CountryLayoutComponent implements OnInit {
  constructor() {}

  ngOnInit() {}
}
