import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-wishlist',
  templateUrl: './wishlist.component.html',
  styleUrls: ['./wishlist.component.css'],
})
export class WishlistComponent implements OnInit {
  wishlist: any[] = [];
  private wishlistUrl = 'http://localhost:5000/api/Wishlist';

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getWishlist();
  }

  getWishlist(): void {
    this.http.get<number[]>(this.wishlistUrl).subscribe(
      (data) => {
        this.wishlist = data;
      },
      (error) => {
        console.error('Error al obtener wishlist:', error);
      }
    );
  }

  eliminarDeWishlist(productId: number): void {
    this.http.delete(`${this.wishlistUrl}/${productId}`).subscribe(
      (response) => {
        alert('Producto eliminado de la lista de deseos');
        this.wishlist = this.wishlist.filter(id => id !== productId);
      },
      (error) => {
        alert('Error al eliminar el producto: ' + error.error);
      }
    );
  }

  obtenerProductoPorId(productId: number) {
  }
}
