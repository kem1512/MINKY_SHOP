﻿// Import the functions you need from the SDKs you need
import { initializeApp } from "https://www.gstatic.com/firebasejs/9.17.1/firebase-app.js";
import { getStorage, ref, listAll, getDownloadURL, uploadBytes } from "https://www.gstatic.com/firebasejs/9.17.1/firebase-storage.js";
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration
// For Firebase JS SDK v7.20.0 and later, measurementId is optional
const firebaseConfig = {
    apiKey: "AIzaSyBXtrCKY7bnByz6gOVzZrum_5FZJqYG__k",
    authDomain: "minkyshop-bed92.firebaseapp.com",
    projectId: "minkyshop-bed92",
    storageBucket: "minkyshop-bed92.appspot.com",
    messagingSenderId: "340483115748",
    appId: "1:340483115748:web:62f36125d56e42cdcf3af1",
    measurementId: "G-4MPKL5WTFD"
};

// Initialize Firebase
const app = initializeApp(firebaseConfig);

const storage = getStorage();

// Create a reference under which you want to list
const listRef = ref(storage, 'images/');

async function storageImages() {
    var res = await listAll(listRef)
    var itemRef = await res.items;
    var images = [];
    for (var x of itemRef) {
        images.push(await getDownloadURL(x))
    }
    return images;
}

async function uploadImages(images) {
    //for (var image of images) {
    //    //const storageRef = ref(storage, 'images/' + image.name);
    //    //uploadBytes(storageRef, new File([image], image.name, { type: image.contentType })).then((snapshot) => {
    //    //    console.log('Uploaded a blob or file!');
    //    //});
    //    console.log(new File([image], image.name, { type: image.contentType }));
    //}
    console.log(images)
}

window.storageImages = storageImages;
window.uploadImages = uploadImages;