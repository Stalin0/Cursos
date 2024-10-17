import { Routes } from '@angular/router';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { ProductosComponent } from './components/productos/productos.component';
import { WishlistComponent } from './components/wishlist/wishlist.component';

export const routes: Routes = [
   { path: '', component: DashboardComponent },
    { path: 'Components/Productos', component: ProductosComponent },
    { path: 'Components/Wishlist', component: WishlistComponent },

];
