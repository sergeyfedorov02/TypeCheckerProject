language core;

extend with #references,
            #natural-literals,
            #top-type,
            #bottom-type,
            #structural-subtyping;

fn main(n : Nat) -> &Top {
  return (fn(x : Nat) { return new(12) })(n)
}