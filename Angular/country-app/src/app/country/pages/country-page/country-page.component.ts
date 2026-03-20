import { Component, Inject, inject, Injectable, signal } from '@angular/core';
import { CountryService } from '../../services/country.service';
import { rxResource } from '@angular/core/rxjs-interop';
import { of } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { NotFoundComponent } from '../../../shared/components/not-found/not-found.component';

@Component({
  selector: 'app-country-page',
  templateUrl: './country-page.component.html',
  imports: [NotFoundComponent],
})
export class CountryPageComponent {
  countryCode = inject(ActivatedRoute).snapshot.params['code'];
  countrySevices = inject(CountryService);
  countryResource = rxResource({
    params: () => ({ code: this.countryCode }),
    stream: ({ params }) => {
      return this.countrySevices.searchByCountryByAlphaCode(params.code);
    },
  });
}
