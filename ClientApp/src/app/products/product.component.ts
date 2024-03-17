import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

interface Product {
  id: number;
  title: string;
  thumbnail: string; 
  price: number; 
}

@Component({
  selector: 'app-products',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductsComponent implements OnInit {
  productList: Product[] = [];
  loading: boolean = true;
  error: string = '';

  constructor(
    private router: Router,
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string
  ) {}

  ngOnInit() {
    this.getAllProducts();
  }

  getAllProducts() {
    this.http.get<Product[]>(this.baseUrl + 'products')
      .subscribe(
        (data) => {
          console.log('Products fetched successfully:', data);
          this.productList = data;
          this.loading = false;
        },
        (error) => {
          console.error('Error fetching products', error);
          this.loading = false;
          this.error = 'An error occurred while fetching products. Please try again.';
        }
      );
  }

  redirectToProductDetail(productId: number) {
    this.router.navigate(['/product-detail', productId]);
  }
}
