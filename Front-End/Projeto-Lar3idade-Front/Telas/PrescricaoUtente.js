import React, { useEffect, useState } from 'react';
import { StyleSheet, ImageBackground, Text, View, FlatList } from 'react-native'; 
import axios from 'axios'; 

export default function PrescricaoMedica({  navigation }) {
 

  return (
    <View style={styles.container}>
      <ImageBackground source={require('../Image/Image2.png')} style={[styles.Image2, styles.bottomImage]} />
      <Text>Prescrições Médicas</Text>
  
    </View>
  );
}
const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#fff',
    alignItems: 'center',
    justifyContent: 'center',
    padding: 16,
  },
  TEXTO: {
    marginBottom: 7,
    borderWidth: 1,
    borderColor: 'black',
    padding: 6,
    width: 'auto',
    borderRadius: 8,
    textAlign: 'center',
  },
  PerfilUtente1: {
    flex: 1,
    padding: 10,
    marginBottom: 50,
    marginTop: 10,
  },
  Image2: {
    height: 205,
    width: '110%',
    padding: 10,
    marginTop: 20,
  },
  bottomImage: {
    position: 'absolute',
    bottom: -125,
  },
});
