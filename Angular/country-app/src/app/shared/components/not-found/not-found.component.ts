import { Location } from '@angular/common';
import { Component, inject } from '@angular/core';

@Component({
  templateUrl: './not-found.component.html',
  selector: 'app-not-found',
})
export class NotFoundComponent {
  location = inject(Location);
  goBack() {
    this.location.back();
  }
}
