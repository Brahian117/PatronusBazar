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
  public Name: string|null="";

  constructor(private cartService: CartService) {
      this.Name = localStorage.getItem("name");
    
  }

  public isUserAuthenticated() {
    const token = localStorage.getItem("jwt");

    if (token) {
      return true;
    }
    else {
      return false;
    }
  }
  public logOut = () => {
    localStorage.removeItem("jwt");
  }


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
