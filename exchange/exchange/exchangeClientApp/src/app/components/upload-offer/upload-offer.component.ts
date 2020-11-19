import { ToastrService } from 'ngx-toastr';
import { element } from 'protractor';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-upload-offer',
  templateUrl: './upload-offer.component.html',
  styleUrls: ['./upload-offer.component.css'],
})
export class UploadOfferComponent implements OnInit {
  constructor(private _toaster: ToastrService) {}

  ngOnInit(): void {}

  onFileChange($event) {
    let file: File = $event.target.files[0];
    if (!file.type.includes('image')) {
      $event.target.value = '';
      this._toaster.error('only image can be upload');
      return;
    }
    console.log('good');
  }
}
