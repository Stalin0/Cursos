import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { RouterLink, RouterModule } from '@angular/router';
import { NgIf, NgFor, CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-productos',
  standalone: true,
  imports: [HttpClientModule, RouterLink, RouterModule, NgIf, NgFor, CommonModule],
  templateUrl: './productos.component.html',
  styleUrls: ['./productos.component.css'],
})
export class ProductosComponent implements OnInit {
  productos: any[] = [];
  wishlist: { productId: number; addedDate: Date }[] = []; 
  precioPromedio: number = 0;

  private apiUrl = 'http://localhost:5000/api/Productos';
  private wishlistUrl = 'http://localhost:5000/api/Wishlist';
  private averageUrl = 'http://localhost:5000/api/Productos/byPrecio'

  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit(): void {
    this.getProductos();
    this.getWishlist();
    this.getAveragePrecio();
  }

  getProductos(): void {
    this.http.get<any[]>(this.apiUrl).subscribe(
      (data) => {
        this.productos = data;
      },
      (error) => {
        console.error('Error al obtener productos:', error);
      }
    );
  }

  getWishlist(): void {
    this.http.get<number[]>(this.wishlistUrl).subscribe(
      (data) => {
        this.wishlist = data.map(id => ({ productId: id, addedDate: new Date(0) })); 
      },
      (error) => {
        console.error('Error al obtener wishlist:', error);
      }
    );
  }

  agregarAWishlist(productId: number) {
    const url = `${this.wishlistUrl}?productId=${productId}`;
  
    this.http.post(url, {}).subscribe({
      next: (response) => {
        console.log('Producto agregado a la lista de deseos:', response);
        this.wishlist.push({ productId, addedDate: new Date() }); 
        alert('Producto agregado a la lista de deseos');
      },
      error: (error) => {
        if (error.status === 400) {
          alert('El producto ya está en la lista de deseos');
        } else {
          console.error('Error al agregar a la lista de deseos:', error);
        }
      }
    });
  }

  eliminarDeWishlist(productId: number): void {
    this.http.delete(`${this.wishlistUrl}/${productId}`).subscribe(
      (response) => {
        alert('Producto eliminado de la lista de deseos');
        this.wishlist = this.wishlist.filter(item => item.productId !== productId); 
      },
      (error) => {
        alert('Error al eliminar el producto: ' + error.error);
      }
    );
  }

  isInWishlist(productId: number): boolean {
    return this.wishlist.some(item => item.productId === productId); 
  }

  getAddedDate(productoId: number): string {
    const item = this.wishlist.find(item => item.productId === productoId);
    return item ? item.addedDate.toString() : 'No disponible'; 
  }

  getAveragePrecio(): void {
    this.http.get<number>(this.averageUrl).subscribe(
      (promedio) => {
        this.precioPromedio = promedio;
        console.log('Precio promedio:', this.precioPromedio); 
      },
      (error) => {
        console.error('Error al obtener el promedio de precios:', error);
      }
    );
  }
    
}
