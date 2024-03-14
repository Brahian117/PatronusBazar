// products.component.ts
import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-products',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductsComponent implements OnInit {
  productList: any[] = [];
  filteredProducts: any[] = [];
  searchTerm: string = '';
  selectedFilter: string = 'price';

  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit(): void {
    this.http.get<any[]>('./assets/products.json').subscribe(data => {
      this.productList = data;
      this.filteredProducts = [...data]; // Initialize filteredProducts with all products
      this.filterProducts(); // Call filterProducts initially to show all products
    });
  }

  redirectToProductDetail(productId: number) {
    this.router.navigate(['/product-detail', productId]);
  }
  filterProducts() {
    this.filteredProducts = this.productList
      .filter(product => product.title.toLowerCase().includes(this.searchTerm.toLowerCase()))
      .sort((a, b) => {
        if (this.selectedFilter === 'price') {
          return a.price - b.price;
        } else if (this.selectedFilter === 'name') {
          return a.title.localeCompare(b.title);
        }
        return 0; // Default case if no valid filter is selected
      });
  }
}
