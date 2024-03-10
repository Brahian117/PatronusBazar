import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

interface Product {
  id: number;
  name: string;
  // Add other properties as needed
}

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  products: Product[] = [];
  loading: boolean = true;
  error: string = '';

  constructor(private router: Router, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {}

  ngOnInit() {
    this.getAllProducts();
  }

  getAllProducts() {
    this.http.get<Product[]>(this.baseUrl + 'products')
      .subscribe(
        (data) => {
          this.products = data;
          this.loading = false;
          console.log(data);
        },
        (error) => {
          console.error('Error fetching products', error);
          this.loading = false;
          this.error = 'An error occurred while fetching products. Please try again.';
        }
      );
  }

  navigateToDetails(productId: number) {
    // Add logic to navigate to the details route with the selected product ID
    this.router.navigate(['/product-details', productId]);
  }
}
