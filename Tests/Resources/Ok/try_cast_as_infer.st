language core;

extend with #exceptions, #try-cast-as;

fn main(n : Nat) -> Bool {
  return (fn (n : Nat) {return try { 0 } cast as Bool { x => x } with { false }}) (0)
}