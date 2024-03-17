import { Component, OnInit } from '@angular/core';
import { CartService } from '../cart/cart.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css'],
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  itemCount: number = 1;

  constructor(private cartService: CartService) { }

  ngOnInit(): void {
    this.updateItemCount();
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  updateItemCount() {
    this.itemCount = this.cartService.getItems().length;
    console.log(this.itemCount)
  }
}
