Proje Bilgileri


| Unity Versiyonu | 6000.2.14f1 |
| Render Pipeline | Built-in / URP / HDRP |
| Case Süresi | 12 saat |
| Tamamlanma Oranı | %75 |

---

## Kurulum

1. Repository'yi klonlayın:
```bash
git clone https://github.com/ilkaydurgun/Project_Alpha.git
```

---

## Eksik kısımlar
Proje kapsamındaki temel oyun mekaniklerinin kod (script) altyapısı tamamlanmıştır. Ancak Unity Editör tarafındaki entegrasyon ve referans atama süreçlerinde yaşanan zaman kısıtları/teknik aksaklıklar nedeniyle bazı modüller henüz tam işlevsel hale getirilememiştir. Özellikle Raycast tabanlı etkileşim sisteminin sahne üzerindeki fizik katman (layer) ayarları ve Kullanıcı Arayüzü (UI) elemanlarının veri bağlama (data binding) işlemleri, kod tarafında hazır olmasına rağmen editör üzerinde nihai yapılandırma aşamasındadır.
### Kontroller

| Tuş | Aksiyon |
|-----|---------|
| WASD | Hareket |
| Mouse | Bakış yönü |
| E | Etkileşim |



### Kullanılan Design Patterns

| Pattern | Kullanım Yeri | Neden |
|---------|---------------|-------|
| [Command pattern] | [IActivatable] | [Oyun içerisindeki etkileşimli objelerin (tuzaklar, kapılar vb.) tetiklenmesi için IActivatable arayüzü kullanıldı. Bu sayede butona basma eylemi (Invoker) ile eylemi gerçekleştiren obje (Receiver) birbirinden soyutlanarak kodun genişletilebilirliği artırıldı.] |

| [State pattern] | [PlayerState] | [Karakter animasyonları ve hareket mekaniği arasındaki senkronizasyon, Unity Animator sistemi kullanılarak Sonlu Durum Makinesi (Finite State Machine) mantığıyla kurgulandı.] |

| [Flyweight Pattern] | [Envanter Sistemi ] | [ Envanter sisteminde her bir eşyanın ortak özelliklerini (isim, açıklama, ikon vb.) her kopya için tekrar tekrar oluşturmak yerine; ScriptableObject'ler kullanılarak verilerin paylaşımlı tek bir kaynaktan okunması sağlandı. Bu sayede CPU ve RAM kullanımında optimizasyon hedeflendi.] |

---



### Zorlandığım Noktalar
>isimlerdirme kısmında sıkıntı çektim çünkü hazır prefablar kulndığım için


---