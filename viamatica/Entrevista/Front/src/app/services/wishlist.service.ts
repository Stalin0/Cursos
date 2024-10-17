import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WishlistService {
  private apiUrl = 'http://localhost:5000/api/Wishlist';

  constructor(private http: HttpClient) {}

  agregarAWishlist(productId: number): Observable<any> {
    return this.http.post(this.apiUrl, { productId });
  }
}
