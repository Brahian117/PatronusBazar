// carousel-banner.component.ts
import { Component, OnInit } from '@angular/core';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { interval } from 'rxjs';
import { takeWhile } from 'rxjs/operators';

@Component({
  selector: 'app-carousel-banner',
  templateUrl: './carousel-banner.component.html',
  styleUrls: ['./carousel-banner.component.css'],
  animations: [
    trigger('fadeInOut', [
      state('in', style({ opacity: 1 })),
      transition(':enter', [style({ opacity: 0 }), animate(300)]),
      transition(':leave', animate(300, style({ opacity: 0 }))),
    ]),
  ],
})
export class CarouselBannerComponent implements OnInit {
  images = ["../../assets/image/banner1.jpg","../../assets/image/banner2.jpg",'../../assets/image/harry.png', "https://m.media-amazon.com/images/I/51XVzc0JcML._AC_UF1000,1000_QL80_.jpg"];
  currentImageIndex = 0;
  fadeInOutState: 'in' | 'out' = 'in';

  ngOnInit(): void {
    // Start auto-slide after component initialization
    interval(3000) // 3 seconds interval
      .pipe(takeWhile(() => true)) // Continue until the component is destroyed
      .subscribe(() => this.autoSlide());
  }

  autoSlide(): void {
    this.fadeInOutState = 'out';
    setTimeout(() => {
      this.currentImageIndex = (this.currentImageIndex + 1) % this.images.length;
      this.fadeInOutState = 'in';
    }, 200); // Animation duration
  }

  nextImage(): void {
    this.fadeInOutState = 'out';
    setTimeout(() => {
      this.currentImageIndex = (this.currentImageIndex + 1) % this.images.length;
      this.fadeInOutState = 'in';
    }, 300); // Animation duration
  }

  prevImage(): void {
    this.fadeInOutState = 'out';
    setTimeout(() => {
      this.currentImageIndex =
        this.currentImageIndex === 0 ? this.images.length - 1 : this.currentImageIndex - 1;
      this.fadeInOutState = 'in';
    }, 300); // Animation duration
  }
}
